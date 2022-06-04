// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.

using NUnit.Framework;
using Sekai.Framework.IO.Storage;

namespace Sekai.Framework.Tests.IO;

public class VirtualStorageTests
{
    [Test]
    public void TestMounting()
    {
        var vfs = new VirtualStorage();
        var a = new InMemoryStorage();
        var b = new InMemoryStorage();
        var root = new InMemoryStorage();

        vfs.Mount("/", root);
        vfs.Mount("/a", a);
        vfs.Mount("/a/b", b);

        Assert.Multiple(() =>
        {
            Assert.That(vfs.GetStorage("/a/"), Is.Not.Null);
            Assert.That(vfs.GetStorage("/a/b/"), Is.Not.Null);
            Assert.That(vfs.GetStorage("/"), Is.Not.Null);
        });
    }
}

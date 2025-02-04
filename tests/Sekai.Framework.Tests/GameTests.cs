// Copyright (c) The Vignette Authors
// Licensed under MIT. See LICENSE for details.

using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Sekai.Framework.Tests;

public class GameTests
{
    private const int wait_time = 30000;

    [Test]
    public void TestGameLifetime()
    {
        using var reset = new ManualResetEvent(false);
        bool gameLoaded = false;

        var game = Game
            .Setup<TestGame>()
            .Build();

        game.OnLoaded += () =>
        {
            gameLoaded = true;
            reset.Set();
        };


        game.OnExiting += () =>
        {
            gameLoaded = false;
            reset.Set();
        };

        var runTask = Task.Factory.StartNew(() => game.Run(), TaskCreationOptions.LongRunning);

        if (!reset.WaitOne(wait_time))
            Assert.Fail("Failed to receive signal in time.");

        Assert.That(gameLoaded, Is.True);

        reset.Reset();
        game.Exit();

        if (!reset.WaitOne(wait_time))
            Assert.Fail("Failed to receive signal in time.");

        Assert.That(gameLoaded, Is.False);
    }
}

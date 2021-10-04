using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TimerTests
{

    private Timer _t;
    
    [Test]
    public void CallabckTest()
    {
        bool testBool = false;

        _t = new Timer(1, () => { testBool = true; });

        // test to see if intialization worked
        Assert.AreEqual(_t.Duration, 1);

        // increase time to end
        _t.Tick(1);

        // testBool should be true at this point
        Assert.True(testBool);
    }

    [Test]
    public void CompletionTest()
    {
        _t = new Timer(1, () => {});

        // increase time to end
        _t.Tick(1);

        // timer should have reset by itself
        Assert.AreEqual(_t.ElapsedTime, 0);
    }

    [Test]
    public void ManualResetTest()
    {
        _t = new Timer(1, () => {});

        // increase timer
        _t.Tick(0.4f);

        // reset timer manually
        _t.Reset();

        // timer should be reset
        Assert.AreEqual(_t.ElapsedTime, 0);
    }

    [Test]
    public void NegativeNumberTest()
    {
        _t = new Timer(1, () => {});

        // should throw an exception here
        Assert.Throws<ArgumentException>(() => { _t.Tick(-1f); } );
    }

}

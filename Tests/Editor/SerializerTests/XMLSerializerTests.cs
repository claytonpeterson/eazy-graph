using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class XMLSerializerTests
{
    [Test]
    public void TestSaving()
    {
        int numberOne = 1;
        int numberTwo = 1;

        Assert.That(numberOne == numberTwo, "numbers are not equal");
    }

    [Test]
    public void TestLoading()
    {

    }
}

using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ItemTagGeneratorTest {

	[Test]
	public void ItemTagGeneratorTestSimplePasses() {
		var itemTagGenerator = new ItemTagGenerator ();

		var itemTag = itemTagGenerator.DispenseTag ();

		Assert.IsNotNull (itemTag.itemName);
		Assert.IsNotNull (itemTag.itemDescription);
	}

	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator ItemTagGeneratorTestWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;
	}
}

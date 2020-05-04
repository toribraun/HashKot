using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class HashKotTests
    {
        [UnityTest]
        public IEnumerator TestUpdatePoints()
        {
            var hashKot = 
                MonoBehaviour.Instantiate(Resources.Load<HashKot>("HashKot"));
            var pointsSumText =
                MonoBehaviour.Instantiate(Resources.Load<Text>("Text"));

            hashKot.UpdatePoints(10);
            yield return new WaitForSeconds(0.1F);
            Assert.AreEqual(10, hashKot.pointsSum);
            Assert.AreEqual("10", pointsSumText.text);
            Object.Destroy(hashKot);
            Object.Destroy(pointsSumText);
        }
        
        [UnityTest]
        public IEnumerator TestUpdatePointsOnCollisionWithPoint2()
        {
            var hashKot = 
                MonoBehaviour.Instantiate(Resources.Load<HashKot>("HashKot"));
            var point = 
                MonoBehaviour.Instantiate(Resources.Load<Point>("Point2"));
            
            hashKot.transform.position = Vector3.MoveTowards(
                hashKot.transform.position, 
                hashKot.transform.position - Vector3.right * (hashKot.transform.position.x - point.transform.position.x), 
                10);
            yield return new WaitForSeconds(0.1F);
            
            Assert.AreEqual(2, hashKot.pointsSum);
            Object.Destroy(hashKot);
            Object.Destroy(point);
        }
        
        [UnityTest]
        public IEnumerator TestGetDamageFromPython()
        {
            var hashKot = 
                MonoBehaviour.Instantiate(Resources.Load<HashKot>("HashKot"));
            var python = 
                MonoBehaviour.Instantiate(Resources.Load<Python>("Python"));

            hashKot.UpdatePoints(10);
            python.transform.position += Vector3.left * 75;
            yield return new WaitForSeconds(0.1F);
            
            Assert.Less(hashKot.pointsSum, 10);
            Object.Destroy(hashKot);
            Object.Destroy(python);
        }
    }
}

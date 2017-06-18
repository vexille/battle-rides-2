using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace LuftSchloss.Persistence {

    public class DummyScriptableObject : ScriptableObject {
        public int value;
    }

    [TestFixture]
    public class ScriptableObjectPersistenceTest {
        
        [Test]
        public void TryToAccessUnloadedData() {
            var persistence = CreatePersistence();
            Assert.That(() => persistence.GetDataCopy(), Throws.InvalidOperationException);
        }

        [Test]
        public void TryToSaveUnloadedData() {
            var persistence = CreatePersistence();
            Assert.That(() => persistence.Save(), Throws.InvalidOperationException);
        }

        [Test]
        public void DispatchOnChangeEvent() {
            bool reached = false;
            var dummy = CreateDummyData();
            dummy.value = 0;

            var persistence = CreatePersistence();
            persistence.SetDataCopy(dummy);
            persistence.AddChangeListener((modifiedObc) => {
                reached = true;
            });

            dummy.value = 25;
            Assert.IsFalse(reached);

            persistence.SetDataCopy(dummy);
            Assert.IsTrue(reached);
        }

        [Test]
        public void SetModifiedData() {
            var dummy = CreateDummyData();
            dummy.value = 0;

            var persistence = CreatePersistence();
            persistence.SetDataCopy(dummy);

            dummy.value = 25;
            Assert.AreEqual(0, persistence.GetDataCopy().value);

            persistence.SetDataCopy(dummy);
            Assert.AreEqual(25, persistence.GetDataCopy().value);
        }

        [Test]
        public void IgnoreOutsideChangesToData() {
            var dummy = CreateDummyData();
            dummy.value = 0;

            var persistence = CreatePersistence();
            persistence.SetDataCopy(dummy);

            dummy.value = 10;

            Assert.AreEqual(0, persistence.GetDataCopy().value);
        }

        [Test]
        public void CreateNewInstanceForNonExistingFile() {
            var persistence = CreatePersistence();
            var data = persistence.Load();

            Assert.IsNotNull(data);
        }

        [Test]
        public void SaveNewFile() {
            var persistence = CreatePersistence();
            var dummy = CreateDummyData();
            dummy.value = 50;

            persistence.SetDataCopy(dummy);
            persistence.Save();

            var savedData = Resources.Load<DummyScriptableObject>("data/" + typeof(DummyScriptableObject).Name);
            Assert.IsNotNull(savedData);
            Assert.AreEqual(50, savedData.value);
        }

        [Test]
        public void OverwriteSavedFile() {
            var data = CreateDummyData();
            data.value = 0;
            SaveDataToResources(data);

            var persistence = CreatePersistence();
            var dummy = CreateDummyData();
            dummy.value = 50;

            persistence.SetDataCopy(dummy);
            persistence.Save();

            var savedData = Resources.Load<DummyScriptableObject>("data/" + typeof(DummyScriptableObject).Name);
            Assert.IsNotNull(savedData);
            Assert.AreEqual(50, savedData.value);
        }

        [Test]
        public void LoadSavedDataFromFile() {
            var data = CreateDummyData();
            data.value = 50;
            SaveDataToResources(data);

            var persistence = CreatePersistence();
            var savedData = persistence.Load();

            Assert.IsNotNull(savedData);
            Assert.AreEqual(50, savedData.value);
        }

        [TearDown]
        public void CleanGeneratedFiles() {
            DeleteDataFromResources();
        }

        private ScriptableObjectPersistence<DummyScriptableObject> CreatePersistence() {
            return new ScriptableObjectPersistence<DummyScriptableObject>();
        }

        private DummyScriptableObject CreateDummyData() {
            return ScriptableObject.CreateInstance<DummyScriptableObject>();
        }

        private void SaveDataToResources(DummyScriptableObject data) {
            AssetDatabase.CreateAsset(data, "Assets/dwarfortinho/Resources/data/" + typeof(DummyScriptableObject).Name + ".asset");
            AssetDatabase.Refresh();
        }

        private void DeleteDataFromResources() {
            var savedData = Resources.Load<DummyScriptableObject>("data/" + typeof(DummyScriptableObject).Name);
            var assetPath = AssetDatabase.GetAssetPath(savedData);
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.Refresh();
        }
    }
}

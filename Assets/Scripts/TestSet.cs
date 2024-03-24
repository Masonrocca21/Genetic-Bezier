using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TestSet : MonoBehaviour
{
    private void Awake() {
        if(!Directory.Exists("./SavedChild")){
            Directory.CreateDirectory("./SavedChild");
        }

        TextAsset textFile4, textFile9, textFile16, textFile25, textFile36, TestSet16, TestSet25, TestSet36;

        textFile4 = (TextAsset)Resources.Load("SavedChild/Default.4", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/Default.4.json",textFile4.text);
        
        textFile9 = (TextAsset)Resources.Load("SavedChild/Default.9", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/Default.9.json",textFile9.text);
        
        textFile16 = (TextAsset)Resources.Load("SavedChild/Default.16", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/Default.16.json",textFile16.text);

        textFile25 = (TextAsset)Resources.Load("SavedChild/Default.25", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/Default.25.json",textFile25.text);

        textFile36 = (TextAsset)Resources.Load("SavedChild/Default.36", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/Default.36.json",textFile36.text);

        TestSet16 = (TextAsset)Resources.Load("SavedChild/TestSet.16", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/TestSet.16.json",TestSet16.text);

        TestSet25 = (TextAsset)Resources.Load("SavedChild/TestSet.25", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/TestSet.25.json",TestSet25.text);

        TestSet36 = (TextAsset)Resources.Load("SavedChild/TestSet.36", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/TestSet.36.json",TestSet36.text);

        TestSet25 = (TextAsset)Resources.Load("SavedChild/TestSet2.25", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/TestSet2.25.json",TestSet25.text);

        TestSet25 = (TextAsset)Resources.Load("SavedChild/TestSet3.25", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/TestSet3.25.json",TestSet25.text);

        TestSet25 = (TextAsset)Resources.Load("SavedChild/TestSet4.25", typeof(TextAsset));
        System.IO.File.WriteAllText("./SavedChild/TestSet4.25.json",TestSet25.text);
    }
}

using System.Collections.Generic;
using UnityEngine;

public struct IntRange
    {
        public int Min;
        public int Max;
        public float Weight;

        public IntRange(int minimo, int massimo, float peso){
            Min = minimo;
            Max = massimo;
            Weight = peso;
        }
    }
public class GeneticAlgorithms 
    {
        private List<Vector3> Child = new List<Vector3>();
        public int Parent1_CP_number=0;

        public List<Vector3> GetChild(){
            return this.Child;
        }

        public void ClearChild()
        {
            Child.Clear();
        }

        public void CrossoverSingle(List<Vector3> Parent1, List<Vector3> Parent2){
            int cut_Index = Random.Range(1, Parent1.Count);
            int dominance = Random.Range(1,3);

            Debug.Log("cut_Index: "+cut_Index);

            /* for(int i=0; i<25; i++)
            {
                Debug.Log("Pos Parent1: "+Parent1[i]);
                Debug.Log("Pos Parent2: "+Parent2[i]);
            } */

            if (dominance == 1) {
                for (int i = 0; i < cut_Index+1; i++){
                    Child.Add(Parent1[i]);
                    Parent1_CP_number +=1;
                }
                for (int i = cut_Index+1; i < Parent1.Count; i++) Child.Add(Parent2[i]);
                
            }
            else {
                for (int i = 0; i < cut_Index+1; i++) Child.Add(Parent2[i]); 
                for (int i = cut_Index+1; i < Parent1.Count; i++){
                    Child.Add(Parent1[i]);
                    Parent1_CP_number+=1;
                }
            }


            /* for(int i=0; i<25; i++)
            {
                Debug.Log("Pos Child: "+Child[i]);
            } */
        }

        public void CrossoverDouble(List<Vector3> Parent1, List<Vector3> Parent2){
            int cut_Index_one = Random.Range(1, Parent1.Count - 1);
            int cut_Index_two = Random.Range(cut_Index_one, Parent1.Count);
            int dominance = Random.Range(1,3);

            if (dominance == 1) {
                for (int i = 0; i < Parent1.Count; i++){
                    if (i < cut_Index_one + 1 || i > cut_Index_two){
                        Child.Add(Parent1[i]);
                        Parent1_CP_number+=1;
                    }
                    else Child.Add(Parent2[i]);
                }
            }
            else{
                for (int i = 0; i < Parent1.Count; i++){
                    if (i < cut_Index_one + 1 || i > cut_Index_two) Child.Add(Parent2[i]);
                    else{
                        Child.Add(Parent1[i]);
                        Parent1_CP_number+=1;
                    }
                }
            }
        }

        public void CrossoverUniform(List<Vector3> Parent1, List<Vector3> Parent2){
            for (int i = 0; i < Parent1.Count; i++){
                if (Random.Range(1,3) == 1){
                    Child.Add(Parent1[i]);
                    Parent1_CP_number+=1;
                }
                else Child.Add(Parent2[i]);
            }
        }

        public void Mutation(){
            for (int i = 0; i < Child.Count; i++)
            {
                int mutation_value = RandomRange.Range(new IntRange(0, 1, 99f), new IntRange(1, 2, 1f));
                Child[i] += new Vector3(mutation_value*Random.Range(-2f, 2f), mutation_value*Random.Range(-2f, 2f), mutation_value*Random.Range(-2f, 2f));
            }
        }
    }

public static class RandomRange
{
    public static int Range(params IntRange[] ranges)
    {
        if (ranges.Length == 0) throw new System.ArgumentException("At least one range must be included.");
        if (ranges.Length == 1) return Random.Range(ranges[0].Min, ranges[0].Max);

        float total = 0f;
        for (int i = 0; i < ranges.Length; i++) total += ranges[i].Weight;

        float r = Random.value;
        float s = 0f;

        int cnt = ranges.Length - 1;
        for (int i = 0; i < cnt; i++)
        {
            s += ranges[i].Weight / total;
            if (s >= r)
            {
                return Random.Range(ranges[i].Min, ranges[i].Max);
            }
        }
        return Random.Range(ranges[cnt].Min, ranges[cnt].Max);
    }
}
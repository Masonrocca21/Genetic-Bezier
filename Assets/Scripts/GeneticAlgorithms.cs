using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
public class GeneticAlgorithms : MonoBehaviour
    {
        Vector3[] Padre = new Vector3[25];
        Vector3[] Madre = new Vector3[25];
        Vector3[] Figlio = new Vector3[25];
    
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void CrossoverSingle(){
            int taglio = Random.Range(1, 25);
            int dominance = Random.Range(1,3);

            if (dominance == 1) {
                for (int i = 0; i < taglio+1; i++) Figlio[i] = Padre[i];
                for (int i = taglio+1; i < 25; i++) Figlio[i] = Madre[i];
            }
            else {
                for (int i = 0; i < taglio+1; i++) Figlio[i] = Madre[i];
                for (int i = taglio+1; i < 25; i++) Figlio[i] = Padre[i];
            }
        }

        public void CrossoverDouble(){
            int taglio_uno = Random.Range(1,24);
            int taglio_due = Random.Range(taglio_uno, 25);
            int dominance = Random.Range(1,3);

            if (dominance == 1) {
                for (int i = 0; i< 25; i++){
                    if (i < taglio_uno + 1 || i > taglio_due) Figlio[i] = Padre[i];
                    else Figlio[i] = Madre[i];
                }
            }
            else{
                for (int i = 0; i< 25; i++){
                    if (i < taglio_uno + 1 || i > taglio_due) Figlio[i] = Madre[i];
                    else Figlio[i] = Padre[i];
                }
            }
        }

        public void CrossoverUniform(){
            for (int i = 0; i < 25; i++){
                if (Random.Range(1,3) == 1) Figlio[i] = Padre[i];
                else Figlio[i] = Madre[i];
            }
        }

        public void Mutation(){
            for (int i = 0; i<25; i++){
                int mutation_value = RandomRange.Range(new IntRange(0, 1, 99f), new IntRange(1, 2, 1f));
                Figlio[i].x += (float) mutation_value*Random.Range(-10, 10);
                Figlio[i].y += (float) mutation_value*Random.Range(-10, 10);
                Figlio[i].z += (float) mutation_value*Random.Range(-10, 10);
                }
        }
    }

public static class RandomRange
    {
        public static int Range(params IntRange[] ranges)
        {
            if (ranges.Length == 0) throw new System.ArgumentException("At least one range must be included.");
            if (ranges.Length == 1) return Random.Range(ranges[0].Max, ranges[0].Min);
 
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
                    return Random.Range(ranges[i].Max, ranges[i].Min);
                }
            }
            return Random.Range(ranges[cnt].Max, ranges[cnt].Min);
        }
    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalloffGenerator
{
    public static float[,] GenerateFalloffMap(int size)
    {
        float[,] map = new float[size,size]; //map de taille carré

        for(int i=0;i<size;i++) 
        {
            for(int j=0;j<size;j++)
            {
                float x = i/(float)size*2-1; //valeur en -1 et 1
                float y = j/(float)size*2-1;
            
                float value = Mathf.Max(Mathf.Abs(x),Mathf.Abs(y)); //valeur la plus haute attribuée à value
                map[i,j]=Evaluate(value);
            }

        } 
        return map;

    }


    static float Evaluate(float value)
    {
        float a = 3f;
        float b=2.2f;

        //on doit faire f(x)=x^a / x^a + (b-bx)^a
        //              f(x)=x^a / x^a + (1-x)^a étant la fct de génération de notre map le b 
        //permet de "pousser" le blanc soit le 0 et donc l'eau le plus loin soit au bord possible
        // dans notre map

        return Mathf.Pow(value,a)/(Mathf.Pow(value,a)+ Mathf.Pow(b-b*value,a)); 
    }
}

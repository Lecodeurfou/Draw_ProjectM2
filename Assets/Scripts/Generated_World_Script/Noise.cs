using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{

	public enum  NormalizeMode { Local, Global};

	public static float[,] GenerateNoiseMap(int mapWidth , int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity , Vector2 offset, NormalizeMode normalizeMode)
	{
		float[,] noiseMap = new float [mapWidth, mapHeight];

		System.Random prng = new System.Random(seed); //pseudo random number generator
		Vector2[] octaveOffsets = new Vector2[octaves]; //génération par offset

		float maxPossibleHeight = 0;
		float amplitude =1;
		float frequency = 1;

		for(int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-100000,+100000) + offset.x ; //gen en x de l'offset; offet.x = offset perso;
			float offsetY = prng.Next(-100000,+100000) - offset.y ; //gen en y de l'offset; offet.y = offset perso;
			octaveOffsets[i] = new Vector2(offsetX,offsetY); 
		
			maxPossibleHeight +=amplitude;
			amplitude = amplitude * persistance;
		}


		if(scale <= 0 )
		{
			scale = 0.0001f;
		}
		
		float maxLocalNoiseHeight = float.MinValue; // normalisation min valeur la plus basse sera 0
		float minLocalNoiseHeight = float.MaxValue; //normalisation max valeur la plus haute sera 1 
		
		float halfWidth = mapWidth / 2 ; //génération au centre plutôt qu'au coin 
		float halfHeight = mapHeight /2 ;

		for(int y = 0; y < mapHeight; y++)
		{
			for (int x=0; x < mapWidth; x++)
			{
				amplitude =1;
				frequency = 1;
				float noiseHeight = 0;

				for(int i=0;i < octaves;i++){
					float sampleX = (x - halfWidth + octaveOffsets[i].x) / scale * frequency  ;
					float sampleY = (y - halfHeight + octaveOffsets[i].y) / scale * frequency ;

					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 -1; //possibilité d'avoir du négatif pour plus de variété 
					noiseHeight += perlinValue * amplitude; //perlin value + amp et elle varie en fct des octaves plus tard

					amplitude = amplitude * persistance;
					frequency = frequency * lacunarity; //freq up par octave 

				}

				if(noiseHeight > maxLocalNoiseHeight) // verification de la normalisation
				{
					maxLocalNoiseHeight = noiseHeight;

				}else if(noiseHeight < minLocalNoiseHeight)
				{
					minLocalNoiseHeight = noiseHeight;
				}

				noiseMap[x,y]= noiseHeight;
			}
		}


		for(int y = 0; y < mapHeight; y++)
		{
			for (int x=0; x < mapWidth; x++)
			{
				if(normalizeMode == NormalizeMode.Local) //en cas de non monde infini cette normalisation du monde est la meilleure
				{
					noiseMap[x,y] = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight , noiseMap[x,y]); // inverslerp revoie une val entre 0 et 1, redefinition du 0 et 1 relatif a leur prétransformation;
				}
				else
				{
					float normalizedHeight = (noiseMap[x,y] + 1 )/(maxPossibleHeight);
					noiseMap[x,y]= Mathf.Clamp(normalizedHeight,0,int.MaxValue);
				}
			}

		}

		return noiseMap;
	}
}
 
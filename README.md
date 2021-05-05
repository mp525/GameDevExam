# GameDevExam

Noell: I added a health and hunger bar to Selina's player. The hunger bar decreases over time and the speed of which it needs to decrease can be modified. 
The bars fills' decrease if the health/hunger decreases. I've never made any kind of bars before so that was quite interesting. I fought with unity for a while over the squares,
that I used for the bars. It ended up being okay. I opted for going for no background on the bars to keep a very clean look, it is still not final though!


Matti: I made 3 AI animals, these are friendly AI's and there primary focus is to just make the world more alive.
The Friendly AI are state based with different factors that decide which state they will be in. 
Idle and jumping, if there is no one around it will sometimes jump or be idle
it will sometimes walk to different random destinations. If an enemy is near it will run away.
There are different animations connected to the script with a method that handles animation, that makes the booleans true and make the animations start.
I have made the dog AI script different than the penguin and chicken because i plan to make it somehow be more connected to the player later.
I plan to make an enemy AI to give a challenge to the player. 


Mathias: Spil-scenen og det ø-miljø man skal overleve i, har jeg lavet. Jeg har brugt et terrain objekt og modelleret på det så det ligner en "tropisk" ø. 
Til textures på terrænet har jeg brugt et "Terrain Painter"-komponent, som alt efter højde og hældning i terrænet, tegner de passende textures på. 
Rundt om øen, skulle der vand(da det er en ø), så jeg har brugt en oceanplane asset der tegner et animeret "terræn" som forestiller et hav.
For ikke at spilleren kan gå for langt ud i vandet, har jeg lavet en usynlig kollider rundt om øen, som stopper spilleren. Dette kan eventuelt ændres, hvis man på et tidspunkt overvejer en svømmefunktion.
Træer og buske er tilføjet til terrænet med et "Vegetation Spawner"-komponent. Dette har gjort det nemt at fylde øen om med træer og buske(mere i fremtiden måske), så øen ikke er tom og ingen stemning har. Dette har dog gjort det vandskeligere at interegere med træerne ud over en collision, da man ikke kan have scripts på terræntreer(åbentbart). Men hjælp fra Jesper, er det lykkedes at give en løsning, der potentielt kan have performance-issues. Fremtidige overvejelser vil gå på, om man skal placere alle træer manuelt, så de har den ønskede effekt, samt god performance.
Day-night cycle også implementeret en prototype af. Indtil videre består dette af 1 directional light(sol), som pejer på øens center. Dette light har jeg fået til at rotere rundt om øen, så man simulerer en rigtig day-night cycle. Skybox er uden sol, da dens sol ikke bevæger sig i takt med vores directional light, og om natten uden måne. 

Scripts necessaire

Retourne la caméra ou la switch pour voir derrière avec un bouton

? changer de vu de tps a fps ?


Script lors des debuts de mission qui met en contexte ce qu'on va faire avec les ojectif met le jeu en pose 
En jeu dialogue qui s'active lorsqu'on effectue une action

Mise en scene

tableau de string avec les replique dedans
Int = indexDialogue
 
a chaque clic on augmenter l'indexDialogue 
j'affiche dans le text la valeur de la du case du tableau du correspondante (a indexDialogue)

Si indexDialogue >= a la taille du tableau => fin du dialogue

Pour avoir les images des personnes remplacer les string par des structures (composé de string avec reference a l'image afficher et possible un deuxieme string avec le nom par exemple)


Unstash l'image des controlle

Changer de camera

Changer la depth de la camera et appliquer des conditions pour les inputs pour pas tout casser (apparement la depth de la camera est juste pour le rendu donc bizarre)
Maybe "Self-learning by evolution" is a more accurate conceptualization for the respective project. The very goal is to show the learning proces on the basis of genetical selection. The cars are programmed with one main-goal: spending so much as possible time on the track without colliding.  

Each car has its own feed-forward neural network. The input layer provides the distances to the walls that surround the cars. The output layer provides the required rotation and acceleration.

Instead of using backpropagation for the neural network to learn, the Genetic Algorithm is used. The car with the best genes will survive and it will propagate the genes to the next generation. They will evolve in a better way and survive more time.

After every iteration the two best cars will be selected. They will swap randomly their the genes in order to create a new (better) DNA. These random DNA-Values are getting automatically modified if the parents have very different or very similar results. Eventually a new set of cars ("the children") will be initialized with the new DNA.

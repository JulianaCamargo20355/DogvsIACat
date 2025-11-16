🇺🇸 English Version

Game Description

You control a dog in a 2D scene, and your goal is to move around the environment and try to reach the cat.
The cat is an NPC controlled by an artificial neural network. It moves around the scene and escapes from the player.
Over time, the neural network learns patterns from the player’s movement, allowing the cat to improve its escape behavior.

Controls

Move: WASD keys or Arrow Keys

Artificial Neural Network (ANN)

The NPC cat uses a simple neural network to adjust its behavior.
The network receives inputs such as:

Player’s position relative to the cat

Cat’s current velocity

Based on these values, the ANN outputs a movement direction.
The cat does not chase the player. It only runs away, and training gradually improves this behavior.

Scripts

The project uses four main scripts:

MovePlayers

Player movement, sprite flipping, running animation, and life system.
If the player loses all lives, the GameOver scene loads.

MoveCat

Simple patrol movement between two X positions, independent from the AI logic.

CatAI

Main AI controller: sends inputs to the neural network, receives movement direction, and applies escape logic when the player is close.
Handles training every few seconds.

NeuralNet

Simple artificial neural network implementation with feedforward and basic weight adjustment.

Play Unity

https://play.unity.com/en/games/ae7d5a3e-3e06-49d6-9c62-e798fddad38b/player-vs-catai

Notes

The project uses Unity 2022.3.45f1.


🇧🇷 Versão em Português

Descrição do Jogo

Você controla um cachorro em uma cena 2D, e seu objetivo é se movimentar pelo ambiente e tentar alcançar o gato.
O gato é um NPC controlado por uma rede neural artificial. Ele se movimenta pela cena e foge do jogador.
Com o tempo, a rede neural aprende padrões do movimento do jogador, fazendo com que o gato melhore o comportamento de fuga.

Controles

Mover: Teclas WASD ou Setas

Rede Neural Artificial (RNA)

O gato utiliza uma pequena rede neural para ajustar seu comportamento.
A rede recebe entradas como:

Posição do jogador em relação ao gato

Velocidade atual do gato

Com base nesses valores, a RNA retorna uma direção de movimento.
O gato não persegue o jogador. Ele apenas foge, e o treinamento melhora esse comportamento gradualmente.

Scripts

O projeto utiliza quatro scripts principais:

MovePlayers

Movimentação do jogador, flip do sprite, animação de corrida e sistema de vidas.
Se as vidas chegarem a zero, a cena GameOver é carregada.

MoveCat

Movimento de patrulha simples entre dois pontos no eixo X, independente da IA.

CatAI

Controlador principal da IA: envia entradas para a rede neural, recebe a direção de movimento e aplica a lógica de fuga quando o jogador está perto.
Realiza treinos periódicos.

NeuralNet

Implementação básica de uma rede neural com feedforward e ajuste simples de pesos.

Play Unity

https://play.unity.com/en/games/ae7d5a3e-3e06-49d6-9c62-e798fddad38b/player-vs-catai

Observações

O projeto utiliza Unity 2022.3.45f1.

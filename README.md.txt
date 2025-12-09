# Runner Game – Unity Project 🏃‍♂️💨

## Descripción 📄
Este proyecto es un juego tipo **Runner con meta final** desarrollado en Unity. El jugador comienza la misión acercándose a un NPC y presionando el botón de inicio. Una vez iniciada, el personaje corre automáticamente y debe llegar a la meta sin chocar con obstáculos.

Se puede cambiar de carril y saltar para esquivar los obstáculos generados dinámicamente.

> **Mecánica de muerte:** Si el jugador colisiona con un obstáculo, se reproduce una animación de muerte y el personaje es regresado automáticamente al inicio, donde necesita volver a acercarse al NPC para iniciar de nuevo la misión. **No existe una pantalla de Game Over.**

## Características ✨
* 🤖 **NPC interactivo** que activa el inicio de la misión.
* 🏁 **Meta final:** El objetivo es completar el recorrido.
* 🏃 **Movimiento automático** hacia adelante.
* 🛣️ **Sistema de tres carriles** con cambio lateral suave.
* 🦘 **Salto funcional** para evitar obstáculos.
* 🚧 **Generación dinámica** de obstáculos.
* ☠️ **Animación de muerte** y reinicio automático frente al NPC.
* ⭐ **Interfaz UI** con contador de estrellas y tiempo.

## Controles 🎮

| Acción | Tecla |
| :--- | :---: |
| Mover a carril izquierdo | `←` |
| Mover a carril derecho | `→` |
| Saltar | `Espacio` |

## Cómo se juega 🕹️
1.  **Inicio:** El jugador aparece frente al NPC.
2.  **Interacción:** Acércate al NPC para mostrar el mensaje y el botón de inicio.
3.  **Carrera:** Presiona el botón y comienza el movimiento automático.
4.  **Esquivar:** Usa las flechas y el espacio para cambiar de carril o saltar los obstáculos.
5.  **Fallo:** Si chocas, se activa la animación de muerte y vuelves al punto de inicio.
6.  **Reintento:** Habla de nuevo con el NPC para reiniciar la misión.
7.  **Victoria:** Llega a la meta para completar el recorrido.

## Estructura del Proyecto 📂
* `Scripts/` – Control del jugador, NPC, generador de obstáculos, UI, prefabs.
* `Prefabs/` – Jugador, obstáculo, estrella, NPC, meta.
* `Scenes/` – Escena principal del juego.
* `UI/` – Canvas de inicio, contador de estrellas y tiempo.

## Requisitos 🛠️
* Unity 2021 o superior.
* S.O.: Windows, macOS o Linux.

## Cómo ejecutar el proyecto 🚀

1.  Clonar el repositorio:
    ```bash
    git clone [https://github.com/usuario/repositorio.git](https://github.com/usuario/repositorio.git)
    ```
2.  Abrir **Unity Hub**.
3.  Dar clic en **Add** y seleccionar la carpeta del proyecto clonado.
4.  Abrir la escena principal dentro de la carpeta `Scenes`.
5.  Presionar el botón **Play**.

---

## Autor ✒️
**Developed by:** Angélica Guerrero Olvera

---

## Licencia 📄
Proyecto libre para estudio y uso personal.
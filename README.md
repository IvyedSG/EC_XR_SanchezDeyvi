# EC_XR_SanchezDeyvi - XR Interaction Challenge

## Información del Estudiante
- **Apellidos y Nombres:** Sanchez Deyvi
- **Código del Estudiante:** 2231894058
- **Curso:** Laboratorio de Realidad Extendida (XR) para Videojuegos
- **Docente:** Victor Alejandro Arroyo Castro

## Funcionalidades Implementadas

### 1. Configuración del Proyecto 
- Proyecto Unity configurado con Universal Render Pipeline (URP)
- XR Plugin Management con OpenXR
- XR Interaction Toolkit 3.3.2
- Input System integrado
- El proyecto ejecuta sin errores

### 2. Escenario XR
- Escena denominada `EC_XR_SanchezDeyvi`
- Piso de 10x10 unidades
- Iluminación direccional + ambiental
- 4 paredes como límites visuales
- 6 objetos 3D: Cubo rojo, Esfera azul, Cilindro verde, Cápsula amarilla, Llave morada, Mesa

### 3. Interacción con Objetos
- **Cubo rojo** y **Esfera azul** manipulables con `XRGrabInteractable`
- Componentes `Rigidbody` para física
- **Llave morada** también manipulable
- Los objetos se pueden agarrar, mover y soltar

### 4. Interacción a Distancia
- **Interruptor de luz** en la pared norte
- Interacción mediante rayo (`XRRayInteractor`)
- Permite encender/apagar la luz de la escena
- Cambia el color del interruptor (blanco = encendido, gris = apagado)

### 5. Reto Libre
- **Teletransporte**: Área de teletransporte en el suelo (plano azul semitransparente)
- Sistema de locomoción con `TeleportationProvider`
- **Snap Turn**: Rotación de 45° con el stick del controlador

## Controles e Instrucciones

### En Unity Editor
1. Abrir la escena `Assets/Scenes/EC_XR_SanchezDeyvi.unity`
2. Presionar Play
3. Usar el XR Device Simulator (XRI) para simular controles


## Tecnologías y Paquetes Utilizados
- **Unity 6000.3.10f1** (6.3 LTS)
- **Universal Render Pipeline (URP)** 17.3.0
- **XR Interaction Toolkit** 3.3.2
- **XR Plugin Management** 4.5.4
- **OpenXR Plugin** 1.16.1
- **Input System** 1.18.0
- **XR Core Utils** 2.5.1

## Estructura del Proyecto
```
Assets/
├── Editor/
│   └── BuildXRScene.cs          # Script de generación de escena
├── Materials/                   # Materiales de los objetos
├── Scenes/
│   └── EC_XR_SanchezDeyvi.unity  # Escena principal
├── Scripts/
│   └── RayInteraction.cs        # Script de interacción por rayo
├── Settings/                    # Configuración URP y XR
└── XR/                          # Configuración XR
```


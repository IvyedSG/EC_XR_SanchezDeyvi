# EC_XR_SanchezDeyvi

Proyecto de Unity con **Universal Render Pipeline (URP)**.

> **Estado:** proyecto inicializado desde la plantilla 3D Cross Platform. Aún no hay
> contenido de juego developed; la base de render, input y settings está configurada.

---

## Requisitos

| Componente | Versión |
|---|---|
| Unity Editor | `6000.3.10f1` (Unity 6.3) |
| Render Pipeline | Universal RP `17.3.0` |
| Input System | `1.18.0` |
| .NET (Scripting Runtime) | .NET Standard 2.1 |

**Importante:** todos los colaboradores deben usar **exactamente la misma versión**
de Unity (ver `ProjectSettings/ProjectVersion.txt`). Las versiones de Unity no son
compatibles hacia atrás en los assets serializados.

---

## Cómo abrir el proyecto

1. Instalar Unity Hub y añadir la versión `6000.3.10f1`.
2. Clonar el repositorio.
3. En Unity Hub: **Add → disco local** y seleccionar la carpeta del proyecto.

La primera apertura tarda varios minutos: Unity reimporta assets y compila todos
los scripts. Es normal.

---

## Estructura

```
EC_XR_SanchezDeyvi/
├── Assets/
│   ├── Scenes/              Escenas del proyecto
│   │   └── SampleScene.unity
│   ├── Settings/            Assets de configuración de URP
│   │   ├── PC_RPAsset.asset         Render pipeline para PC
│   │   ├── Mobile_RPAsset.asset     Render pipeline para mobile
│   │   ├── PC_Renderer.asset
│   │   ├── Mobile_Renderer.asset
│   │   ├── SampleSceneProfile.asset
│   │   └── UniversalRenderPipelineGlobalSettings.asset
│   ├── TutorialInfo/        Assets de la plantilla (no borrar)
│   ├── InputSystem_Actions.inputactions   Acciones de input
│   └── Readme.asset
├── Packages/
│   ├── manifest.json        Dependencias del proyecto
│   └── packages-lock.json   Versiones resueltas (NO editar a mano)
├── ProjectSettings/         Configuración del proyecto
├── .gitignore
└── .gitattributes
```

---

## Convenciones de Git

### Qué se versiona

- `Assets/` — **incluyendo todos los archivos `.meta`**
- `Packages/manifest.json` y `packages-lock.json`
- `ProjectSettings/`
- `.gitignore` y `.gitattributes`

### Qué NO se versiona

- `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `Builds/` — cachés regenerables
- `*.csproj`, `*.slnx` — generados por el IDE

> **Nunca borres los `.meta`.** Son la fuente de verdad de los GUID; sin ellos Unity
> pierde las referencias entre assets y la escena se rompe.

---

## Git LFS

El repo usa **Git LFS** para assets binarios (imágenes, audio, video, modelos 3D).
Los archivos `.unity`, `.prefab` y `.asset` están marcados como binarios con
mergeos personalizados para evitar conflictos de texto ilegibles.

Clonar requiere LFS instalado:

```powershell
git lfs install
git lfs pull
```

Ver estado de LFS:

```powershell
git lfs ls-files
```

---

## Validación por CLI

Se puede verificar que el proyecto compila **sin abrir el Editor**, útil para CI:

```powershell
$p = Start-Process "C:\Program Files\Unity\Hub\Editor\6000.3.10f1\Editor\Unity.exe" `
  -ArgumentList @(
    "-batchmode","-nographics","-quit",
    "-projectPath","$PWD",
    "-logFile","$env:TEMP\unity-verify.log"
  ) -Wait -PassThru
$p.ExitCode   # 0 = OK
```

Errores de compilación aparecen en el log como `error CS....`.
Un código de salida distinto de `0` indica fallo.

> Nota: PowerShell **no espera** a `Unity.exe` con el operador `&` (es una app GUI).
> Usa siempre `Start-Process -Wait` para poder leer el exit code.

---

## Problemas conocidos

**Unity se cierra al abrir el proyecto.**

Causa: caché de paquetes corrupta. El Editor compila en batch y aborta sin aviso.

Solución:

```powershell
# 1. Cerrar Unity por completo (verificar que no queden procesos)
Get-Process Unity,UnityPackageManager -ErrorAction SilentlyContinue | Stop-Process -Force

# 2. Limpiar caché de compilación y estado de paquetes
Remove-Item ".\Library\PackageManager",".\Library\Bee",".\Library\ScriptAssemblies",
            ".\Library\Artifacts" -Recurse -Force -ErrorAction SilentlyContinue

# 3. Abrir de nuevo — Unity reextrae los paquetes
```

Si persiste, borrar `Library/` completo (se regenera, pero tarda más).

---

## Licencia

Unity Personal Edition.

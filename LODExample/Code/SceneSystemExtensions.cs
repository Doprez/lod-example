
namespace Stride.Engine;
public static class SceneSystemExtensions
{
	public static CameraComponent TryGetMainCamera(this SceneSystem sceneSystem)
	{
		CameraComponent camera = null;
		if (sceneSystem.GraphicsCompositor.Cameras.Count == 0)
		{
			// The compositor wont have any cameras attached if the game is running in editor mode
			// Search through the scene systems until the camera entity is found
			// This is what you might call "A Hack"
			foreach (var system in sceneSystem.Game.GameSystems)
			{
				if (system is SceneSystem editorSceneSystem)
				{
					foreach (var entity in editorSceneSystem.SceneInstance.RootScene.Entities)
					{
						if (entity.Name == "Camera Editor Entity")
						{
							camera = entity.Get<CameraComponent>();
							break;
						}
					}
				}
			}
		}
		else
		{
			camera = sceneSystem.GraphicsCompositor.Cameras[0].Camera;
		}

		return camera;
	}

	public static Entity TryGetCameraEntity(this SceneSystem sceneSystem)
	{
		Entity camera = null;
		if (sceneSystem.GraphicsCompositor.Cameras.Count == 0)
		{
			// The compositor wont have any cameras attached if the game is running in editor mode
			// Search through the scene systems until the camera entity is found
			// This is what you might call "A Hack"
			foreach (var system in sceneSystem.Game.GameSystems)
			{
				if (system is SceneSystem editorSceneSystem)
				{
					foreach (var entity in editorSceneSystem.SceneInstance.RootScene.Entities)
					{
						if (entity.Name == "Camera Editor Entity")
						{
							camera = entity;
							break;
						}
					}
				}
			}
		}
		else
		{
			camera = sceneSystem.GraphicsCompositor.Cameras[0].Camera.Entity;
		}

		return camera;
	}
}

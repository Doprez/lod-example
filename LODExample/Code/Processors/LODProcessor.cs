using LODExample.Code.EntityComponents;
using Stride.Core.Annotations;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Rendering;
using Stride.UI;
using Stride.UI.Controls;

namespace LODExample.Code.Processors;
public class LODProcessor : EntityProcessor<LODEntityComponent>
{
	public LODProcessor() : base(typeof(LODEntityComponent), typeof(ModelComponent))
	{
		
	}

	public override void Draw(RenderContext context)
	{
		var sceneSystem = context.Services.GetService<SceneSystem>();
		var camera = sceneSystem.TryGetMainCamera();
		if (camera == null)
			return;

		var values = ComponentDatas.Values;

		foreach(var value in values)
		{
			UpdateLOD(camera, value);
		}
	}

	private void UpdateLOD(CameraComponent camera, LODEntityComponent lod)
	{
		var lods = lod.LOD;

		var distance = Vector3.Distance(camera.Entity.Transform.WorldMatrix.TranslationVector, lod.Entity.Transform.WorldMatrix.TranslationVector);

		if(lod.UITest != null && lod.UITest.Page != null)
		{
			var text = lod.UITest.Page.RootElement.FindVisualChildOfType<TextBlock>();
			text.Text = $"Distance: {distance}";
		}

		for (int i = lods.Count - 1; i >= 0; i--)
		{
			if (distance < lods[i].DistanceToActivate && lod.Component.Model != lods[i].Model)
			{
				lod.Component.Model = lods[i].Model;
			}
		}
	}

	protected override void OnEntityComponentAdding(Entity entity, [NotNull] LODEntityComponent component, [NotNull] LODEntityComponent data)
	{
		base.OnEntityComponentAdding(entity, component, data);
		data.Component = entity.Get<ModelComponent>();
	}
}

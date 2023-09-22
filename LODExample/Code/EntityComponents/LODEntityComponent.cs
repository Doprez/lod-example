using LODExample.Code.Processors;
using Stride.Core;
using Stride.Engine;
using Stride.Engine.Design;
using Stride.Rendering;
using System.Collections.Generic;

namespace LODExample.Code.EntityComponents;
[DataContract(nameof(LODEntityComponent))]
[ComponentCategory("Model")]
[DefaultEntityComponentProcessor(typeof(LODProcessor), ExecutionMode = ExecutionMode.All)]
public class LODEntityComponent : EntityComponent
{
	[DataMemberIgnore]
	public ModelComponent Component { get; set; }
	public UIComponent UITest { get; set; }
	public List<LODData> LOD { get; set; } = new();
}

[DataContract(nameof(LODData))]
public class LODData
{
	public Model Model { get; set; }
	public float DistanceToActivate { get; set; } = 0;
}

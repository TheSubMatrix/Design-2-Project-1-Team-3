using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/EnemySpotted")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "EnemySpotted", message: "Spotted [targets]", category: "Events", id: "37a11cb7b1dcb50978a7b9387052415c")]
public sealed partial class Spotted : EventChannel<List<GameObject>> { }


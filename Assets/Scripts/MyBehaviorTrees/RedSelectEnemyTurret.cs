using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("MyTasks")]
[TaskDescription("Select green non targeted enemy turret")]

public class RedSelectEnemyTurret : Action
{
    IArmyElement m_ArmyElement;
    public SharedTransform target;
    public SharedFloat minRadius;
    public SharedFloat maxRadius;

    public override void OnAwake()
    {
        m_ArmyElement = (IArmyElement)GetComponent(typeof(IArmyElement));
    }

    public override TaskStatus OnUpdate()
    {
        if (m_ArmyElement.ArmyManager == null) return TaskStatus.Running; // la référence à l'armée n'a pas encore été injectée
        //printf(typeof(IArmyElement))
        target.Value = m_ArmyElement.ArmyManager.GetRandomWeakEnemies<Turret>() != null ? m_ArmyElement.ArmyManager.GetRandomWeakEnemies<Turret>()?.transform : m_ArmyElement.ArmyManager.GetRandomEnemyOfTypeByDistance<Turret>(transform.position, minRadius.Value, maxRadius.Value)?.transform;
        //m_ArmyElement.ArmyManager.GetRandomEnemy()?.transform;

        if (target.Value != null) return TaskStatus.Success;
        else return TaskStatus.Failure;

    }
}
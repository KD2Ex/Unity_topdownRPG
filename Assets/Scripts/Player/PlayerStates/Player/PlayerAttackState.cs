using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Attacking(true);
        var target = player.PlayerSensor.GetInDirectionOfCursor();
        if (target)
        {
            Debug.Log($"Result target: {target?.parent?.name}, {target?.position}");
            var dirToTarget = (target.position - player.transform.position).normalized;
            player.DashToTarget(dirToTarget, target);
            player.StartCoroutine(Wait(target));
            return;
        }
        
        Debug.LogWarning("No target in 90 degrees cone");
        Attack();
    }

    private IEnumerator Wait(Transform target)
    {
        var dist = (player.transform.position - target.position).magnitude;
        Debug.Log($"Dist in attack state {dist}");
        yield return new WaitUntil(() => (player.transform.position - target.position).magnitude < 2f);
        player.ResetVelocity();
        Debug.Log($"Dist in attack state after routine{dist}");
        Attack();
    }

    private void Attack()
    {
        var dir = player.GetAttackDir();
        var angle = player.GetAngle(dir);
        
        player.animator.SetFloat(player.hash_angle, angle);
        player.animator.SetTrigger(player.hash_attack);
        player.meleeWeapon.Attack(dir);

        player.attackInput = false;
    }

    public override void Update()
    {
        base.Update();

        //Debug.Log($"Attack input: {player.attackInput}");
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        
        player.animator.ResetTrigger(player.hash_attack);
    }
}
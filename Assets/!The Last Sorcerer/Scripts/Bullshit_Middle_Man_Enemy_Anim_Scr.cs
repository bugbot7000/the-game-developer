using UnityEngine;

public class Bullshit_Middle_Man_Enemy_Anim_Scr : MonoBehaviour
{
    public GameObject actualFuckingEnemy;
    public enemyAI_Script actualFuckingScript;
    public scr_health fuckingHealthScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actualFuckingScript = actualFuckingEnemy.GetComponent<enemyAI_Script>();
        fuckingHealthScript = actualFuckingEnemy.GetComponent<scr_health>();
    }
    public void AssVanish() { actualFuckingScript.AssassinVanish(); }
    public void UnHurtYourself() { fuckingHealthScript.BeUnhurt(); }
    public void ShootFuckingArrow() { actualFuckingScript.ShootArror(); }
    public void FuckingDMShot() { actualFuckingScript.DMShot(); }
    public void FuckingZombieCharge() { actualFuckingScript.ZombieCharge(); }

    public void ResetFuckingAttack() { actualFuckingScript.ResetAttack(); }
    public void PlayFootstepsFX()
    {
        GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.footstep);
    }
    public void PlaySlashFX() { GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.weapon_swing); }
    public void PlaySummonFX() { GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.ghostly_fire_summon); }
    public void PlayMoanFX() { GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.zombie_moan); }
    public void PlayNecroDeathFX()
    {
        GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.necro_die);
    }
    public void PlayMinoDeathFX()
    {
        GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.minotaur_death);
    }
    public void PlayMinoStomp()
    {
        GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.mino_stomp);
    }
    public void PlayMinoSnarlFX()
    {
        GameAudioManager.Instance?.playSFX(GameAudioManager.SFX.bull_snarl);
    }
}

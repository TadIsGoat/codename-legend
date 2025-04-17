using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/*
 * DEAR PROGRAMMER,
 * Keep in mind, that since this code had many itterations, it might be overcomplicated for what it's actually doing.
 * Please rework it to a proper state if u have got the time.
 */

public class WeaponController : MonoBehaviour
{
    private WeaponData weaponData;
    private WeaponAnimator weaponAnimator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private WeaponData.States state;
    private float angle;
    public bool isAttacking {get; private set;} = false;

    //combo vars
    [SerializeField] public float lastAttackTime;
    [SerializeField] private int combo = 0;

    private void Awake()
    {
        weaponData = GetComponent<WeaponData>();
        weaponAnimator = GetComponentInChildren<WeaponAnimator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        if (weaponData.hideOnIdle)
            spriteRenderer.enabled = false;

    }

    private void Update()
    {
        SetState();

        if (state == WeaponData.States.attack)
        {
            weaponAnimator.SetAnimation(angle, state, combo);
        }
        else
        {
            weaponAnimator.SetAnimation(angle, state);
        }

        lastAttackTime -= Time.deltaTime;
    }

    public IEnumerator Attack(float _angle)
    {
        isAttacking = true;
        angle = _angle; //needs to happen before stateChange

        yield return new WaitUntil(() => state == WeaponData.States.attack);

        #region combo shit
        if (lastAttackTime > 0 && combo < WeaponData.attackAnims.Count - 1)
        {
            combo++;
        }
        else
        {
            combo = 0;
        }
        #endregion

        spriteRenderer.enabled = true;

        yield return new WaitForSeconds(weaponAnimator.GetAnimLength());

        lastAttackTime = weaponData.comboTreshhold + weaponAnimator.GetAnimLength();

        if (weaponData.hideOnIdle)
            spriteRenderer.enabled = false;

        isAttacking = false;
    }

    private void SetState()
    {
        if (isAttacking)
        {
            state = WeaponData.States.attack;
        }
        else
        {
            angle = 0;
            state = WeaponData.States.idle;
        }
    }
}
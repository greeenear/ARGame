using UnityEngine;

public enum State {
    Idle,
    Die,
    Block
}

public class EnemyController : MonoBehaviour {
    private float timer;
    public ObjectType type;
    public State state;
    public Animator animator;

    private void OnEnable() {
        timer = 0;
        state = State.Idle;
    }

    private void Update() {
        if (state != State.Die) timer += Time.deltaTime;

        if (timer > 1) {
            gameObject.SetActive(false);
        }
    }

    public void OnDie() {
        animator.SetBool("isDie", false);
        state = State.Idle;
        gameObject.SetActive(false);
    }

    public void OnHit() {
        state = State.Die;
    }
}

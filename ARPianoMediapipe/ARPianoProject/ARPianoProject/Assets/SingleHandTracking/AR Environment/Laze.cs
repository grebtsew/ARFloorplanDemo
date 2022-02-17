using UnityEngine;

public class Laze {
    private Vector3 origin = default, tail = default;
    private LineRenderer laze = default;
    private bool lazeModeOn = default;

    public Laze() {
        laze = GameObject.Find("Laze").GetComponent<LineRenderer>();
    }

    public Laze(Vector3 origin, Vector3 tail) {
        this.origin = origin;
        this.tail = tail;
        laze = GameObject.Find("Laze").GetComponent<LineRenderer>();
    }

    public void moveTo(Vector3 newOrigin, Vector3 newTail) {
        if (laze == null || !lazeModeOn) {
            Debug.Log("Can't provice laze");
        } else {
            if (this.origin == default && this.tail == default) {
                laze.SetPosition(0, newOrigin);
                laze.SetPosition(1, newTail);
            } else {;
                laze.SetPosition(0, Vector3.MoveTowards(origin, newOrigin, 0.005f));
                laze.SetPosition(1, Vector3.MoveTowards(tail, newTail, 0.005f));
                this.origin = newOrigin;
                this.tail = newTail;
            }
        }
    }

    public void enable() {
        lazeModeOn = true;
        laze.enabled = true;
    }

    public void disable() {
        lazeModeOn = false;
        laze.enabled = false;
    }

    public Vector3 Origin { get => origin; set => origin = value; }
    public Vector3 Tail { get => tail; set => tail = value; }
    public bool LazeModeOn { get => lazeModeOn; }
}
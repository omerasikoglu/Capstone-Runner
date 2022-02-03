using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
    int times = 0;

    private void Awake() => GameManager.OnStateChanged += GameManager_OnBeforeStateChanged;
    private void OnDestroy() => GameManager.OnStateChanged -= GameManager_OnBeforeStateChanged;

    private void GameManager_OnBeforeStateChanged(GameState obj)
    {
        if (obj == GameState.Running) Debug.Log($"stateChanged {++times} times");

    }
}



namespace CommandPattern
{
    //The parent class
    public abstract class Command
    {
        //How far should the box move when we press a button
        protected float moveDistance = 1f;

        //Move and maybe save command
        public abstract void Execute(Transform boxTrans, Command command);

        //Undo an old command
        public virtual void Undo(Transform boxTrans) { }

        //Move the box
        public virtual void Move(Transform boxTrans) { }
    }

    // Child classes

    public class MoveForward : Command
    {
        //Called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //Move the box
            Move(boxTrans);

            //Save the command
            InputHandler.oldCommands.Add(command);
        }

        //Undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.forward * moveDistance);
        }

        //Move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.forward * moveDistance);
        }
    }


    public class InputHandler : MonoBehaviour
    {
        //The box we control with keys
        public Transform boxTrans;
        //The different keys we need
        private Command buttonW, buttonS, buttonA, buttonD, buttonB, buttonZ, buttonR;
        //Stores all commands for replay and undo
        public static List<Command> oldCommands = new List<Command>();
        //Box start position to know where replay begins
        private Vector3 boxStartPos;
        //To reset the coroutine
        private Coroutine replayCoroutine;
        //If we should start the replay
        public static bool shouldStartReplay;
        //So we cant press keys while replaying
        private bool isReplaying;


        void Start()
        {
            //Bind keys with commands
            buttonB = new DoNothing();
            buttonW = new MoveForward();
            buttonS = new MoveReverse();
            buttonA = new MoveLeft();
            buttonD = new MoveRight();
            buttonZ = new UndoCommand();
            buttonR = new ReplayCommand();

            boxStartPos = boxTrans.position;
        }



        void Update()
        {
            if (!isReplaying)
            {
                HandleInput();
            }

            StartReplay();
        }


        //Check if we press a key, if so do what the key is binded to 
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                buttonA.Execute(boxTrans, buttonA);
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                buttonB.Execute(boxTrans, buttonB);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                buttonD.Execute(boxTrans, buttonD);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                buttonR.Execute(boxTrans, buttonZ);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                buttonS.Execute(boxTrans, buttonS);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                buttonW.Execute(boxTrans, buttonW);
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                buttonZ.Execute(boxTrans, buttonZ);
            }
        }


        //Checks if we should start the replay
        void StartReplay()
        {
            if (!shouldStartReplay || oldCommands.Count <= 0) return;

            shouldStartReplay = false;

            //Stop the coroutine so it starts from the beginning
            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }

            //Start the replay
            replayCoroutine = StartCoroutine(ReplayCommands(boxTrans));

        }


        //The replay coroutine
        IEnumerator ReplayCommands(Transform boxTrans)
        {
            //So we can't move the box with keys while replaying
            isReplaying = true;

            //Move the box to the start position
            boxTrans.position = boxStartPos;

            for (int i = 0; i < oldCommands.Count; i++)
            {
                //Move the box with the current command
                oldCommands[i].Move(boxTrans);

                yield return new WaitForSeconds(0.3f);
            }

            //We can move the box again
            isReplaying = false;
        }
    }


    public class MoveReverse : Command
    {
        //Called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //Move the box
            Move(boxTrans);

            //Save the command
            InputHandler.oldCommands.Add(command);
        }

        //Undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.forward * moveDistance);
        }

        //Move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.forward * moveDistance);
        }
    }


    public class MoveLeft : Command
    {
        //Called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //Move the box
            Move(boxTrans);

            //Save the command
            InputHandler.oldCommands.Add(command);
        }

        //Undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.right * moveDistance);
        }

        //Move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.right * moveDistance);
        }
    }


    public class MoveRight : Command
    {
        //Called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //Move the box
            Move(boxTrans);

            //Save the command
            InputHandler.oldCommands.Add(command);
        }

        //Undo an old command
        public override void Undo(Transform boxTrans)
        {
            boxTrans.Translate(-boxTrans.right * moveDistance);
        }

        //Move the box
        public override void Move(Transform boxTrans)
        {
            boxTrans.Translate(boxTrans.right * moveDistance);
        }
    }


    //For keys with no binding
    public class DoNothing : Command
    {
        //Called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            //Nothing will happen if we press this key
        }
    }


    //Undo one command
    public class UndoCommand : Command
    {
        //Called when we press a key
        public override void Execute(Transform boxTrans, Command command)
        {
            List<Command> oldCommands = InputHandler.oldCommands;

            if (oldCommands.Count > 0)
            {
                Command latestCommand = oldCommands[oldCommands.Count - 1];

                //Move the box with this command
                latestCommand.Undo(boxTrans);

                //Remove the command from the list
                oldCommands.RemoveAt(oldCommands.Count - 1);
            }
        }
    }


    //Replay all commands
    public class ReplayCommand : Command
    {
        public override void Execute(Transform boxTrans, Command command)
        {
            InputHandler.shouldStartReplay = true;
        }
    }
}
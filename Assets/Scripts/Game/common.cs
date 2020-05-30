using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace common
{
    // class for mouse button information
    public class MouseButton
    {
        int id;
        public bool isClicked;
        public bool isHeld;
        public bool wasHeld;
        public float clickDuration;
        public bool hasClickJustStarted;
        public bool hasClickJustEnded;
        public float dragDistance;
        public Vector3 lastClickPosition = new Vector3(0f, 0f, 0f);

        public MouseButton(int buttonId)
        {
            id = buttonId;
            isClicked = false;
            isHeld = false;
            wasHeld = false;
            clickDuration = 0f;
            hasClickJustEnded = false;
            hasClickJustStarted = false;
            dragDistance = 0f;
        }

        public void Update(bool isClicked)
        {
            this.hasClickJustStarted = !this.isClicked & isClicked;
            this.hasClickJustEnded = this.isClicked & !isClicked;
            this.wasHeld = !isClicked & this.isHeld;

            if (this.isClicked)
                this.clickDuration += Time.deltaTime;
            else
                this.clickDuration = 0;

            if (this.hasClickJustStarted)
                this.lastClickPosition = Input.mousePosition;

            this.isClicked = isClicked;

            this.isHeld = this.clickDuration > Constants.mouseLongestClick;
            this.isHeld |= CheckIfDraging();
            
        }

        private bool CheckIfDraging()
        {
            bool _hasMouseMoved;

            // TODO: this should be changed because all members of MouseButton should
            //       be set in Update function.
            dragDistance = CalculateDragDistance(this.lastClickPosition);
            _hasMouseMoved = dragDistance > Constants.mouseDragThreshold;

            return _hasMouseMoved & this.isClicked;
        }

        private float CalculateDragDistance(Vector3 from)
        {
            return Vector2.Distance(from, Input.mousePosition);
        }

        public override string ToString()
        {
            string result = "";
            if (isClicked)
                result += "isClicked";
            if (isHeld)
            {
                result += " - isHeld - ";
                result += this.lastClickPosition.ToString();
            }

            return result;
        }

    }
    
    // class of unity objects for Entity to hold
    public class UnityObjects
    {
        public readonly GameObject gameObject;
        public UnityObjects(GameObject givenGameObject)
        {
            gameObject = givenGameObject;
        }
    }
    
}

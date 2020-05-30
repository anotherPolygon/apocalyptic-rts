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
        public float clickDuration;
        public bool hasClickJustStarted;
        public bool hasClickJustEnded;
        public Vector3 lastClickPosition = new Vector3(0f, 0f);

        public MouseButton(int buttonId)
        {
            id = buttonId;
            isClicked = false;
            isHeld = false;
            clickDuration = 0f;
            hasClickJustEnded = false;
            hasClickJustStarted = false;
        }

        public void Update(bool isClicked)
        {

            this.hasClickJustStarted = !this.isClicked & isClicked;
            this.hasClickJustEnded = this.isClicked & !isClicked;

            if (this.isClicked)
                this.clickDuration += Time.deltaTime;
            else
                this.clickDuration = 0;

            this.isHeld = this.clickDuration > Constants.mouseLongestClick;

            if (this.hasClickJustStarted)
                this.lastClickPosition = Input.mousePosition;

            this.isClicked = isClicked;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class State
    {
        public Directions facingDirection { get; private set; }
        public State parentState { get; private set; }
        public int x { get; private set; }
        public int y { get; private set; }
        private int minefieldSize;
        private int currentEstimatedCost;

        public State(int x, int y, int minefieldSize, State parentState = null)
        {
            this.x = x;
            this.y = y;
            this.minefieldSize = minefieldSize;
            this.parentState = parentState;
            if (parentState != null)
                this.currentEstimatedCost = parentState.currentEstimatedCost;
            else
                this.currentEstimatedCost = 0;
        }

        public State(int x, int y, int minefieldSize, Directions facingDirection, State parentState = null) 
            : this(x, y, minefieldSize, parentState)
        {
            this.facingDirection = facingDirection;
        }

        private int calculateDistance(State state)
        {
            return Math.Abs(this.x - state.x) + Math.Abs(this.y - state.y);
        }

        public bool isFinalState(State finalState)
        {
            return calculateDistance(finalState) == 0;
        }

        public int calculateEstimatedDistance(int reachCost, State finalState)
        {
            this.currentEstimatedCost += reachCost;
            return this.currentEstimatedCost + calculateDistance(finalState);
        }

        private Directions RotateLeft()
        {
            if (this.facingDirection == Directions.Up)
                return Directions.Left;
            else
                return this.facingDirection - 1;
        }

        private Directions RotateRight()
        {
            if (this.facingDirection == Directions.Left)
                return Directions.Up;
            else
                return this.facingDirection + 1;
        }

        private int GetNewX()
        {
            int result;
            if (this.facingDirection == Directions.Left)
                result = this.x - 1;
            else if (this.facingDirection == Directions.Right)
                result = this.x + 1;
            else
                result = this.x;
            if (result < 0 || result >= minefieldSize)
                result = this.x;
            return result;
        }

        private int GetNewY()
        {
            int result;
            if (this.facingDirection == Directions.Up)
                result = this.y - 1;
            else if (this.facingDirection == Directions.Down)
                result = this.y + 1;
            else
                result = this.y;
            if (result < 0 || result >= minefieldSize)
                result = this.y;
            return result;
        }

        public State RotateLeftAction()
        {
            return new State(this.x, this.y, this.minefieldSize, this.RotateLeft(), this);
        }

        public State RotateRightAction()
        {
            return new State(this.x, this.y, this.minefieldSize, this.RotateRight(), this);
        }

        public State MoveForwardAction()
        {
            return new State(this.GetNewX(), this.GetNewY(), this.minefieldSize, this.facingDirection, this);
        }


    }
}

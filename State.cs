using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class State
    {
        private int distance;
        private Directions facingDirection;
        private State parentState;
        public int x { get; private set; }
        public int y { get; private set; }
        private int minefieldSize;

        public State(int x, int y, int minefieldSize, Directions facingDirection, State parentState = null)
        {
            this.x = x;
            this.y = y;
            this.minefieldSize = minefieldSize;
            this.facingDirection = facingDirection;
            this.parentState = parentState;
        }

        private int calculateDistance(State state)
        {
            return Math.Abs(this.x - state.x) + Math.Abs(this.y - state.y);
        }

        public bool isFinalState(State finalState)
        {
            return calculateDistance(finalState) == 1;
        }





    }
}

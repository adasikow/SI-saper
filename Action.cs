using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace saper
{
    class Action
    {
        private Func<void, void> actionName;

        public Action(Func<void, void> actionName)
        {
            this.actionName = actionName;
        }

        public void Perform(Minesweeper minesweeper)
        {
            minesweeper.actionName();
        }
    }
}

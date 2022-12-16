﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCMEngine
{
    public class PSParser : IFunctionStream
    {
        private LookAheadLangParser parser;

        public PSParser(LookAheadLangParser parser)
        {
            this.parser = parser;
        }

        public bool NextIsLocalFunction()
        {
            return false;  // must look into later
        }

        public bool NextIsFunction()
        {
            if (this.parser.PeekNextKeyword().Equals("function"))
            {
                return true;
            }

            return false;
        }

        private void OnFunction()
        {
            this.parser.NextKeyword(); // function

            var functionName = this.parser.NextKeyword();

            throw new CCCParserSuccessException(functionName, this.parser.StreamOffset);
        }

        public void AdvanceToNextFunction()
        {
            while (true)
            {
                if (NextIsFunction())
                {
                    OnFunction();
                }
                else
                {
                    this.parser.NextKeyword(); // consumed and move forward
                }
            }
        }
    }
}

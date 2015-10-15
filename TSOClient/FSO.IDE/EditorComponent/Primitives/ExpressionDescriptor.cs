﻿using FSO.Files.Formats.IFF.Chunks;
using FSO.IDE.EditorComponent.Model;
using FSO.SimAntics.Engine;
using FSO.SimAntics.Engine.Primitives;
using FSO.SimAntics.Engine.Scopes;
using FSO.SimAntics.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSO.IDE.EditorComponent.Primitives
{
    public class ExpressionDescriptor : PrimitiveDescriptor
    {
        public override PrimitiveGroup Group { get { return PrimitiveGroup.Math; } }
        public override PrimitiveReturnTypes Returns {
            get {
                var op = (VMExpressionOperand)Operand;
                return OpReturn[op.Operator];
            }
        }
        public override Type OperandType { get { return typeof(VMExpressionOperand); } }

        public static Dictionary<VMExpressionOperator, string> OperatorStr = new Dictionary<VMExpressionOperator, string>()
        {
            { VMExpressionOperator.LessThan, "<" },
            { VMExpressionOperator.GreaterThan, ">" },
            { VMExpressionOperator.Equals, "==" },
            { VMExpressionOperator.PlusEquals, "+=" },
            { VMExpressionOperator.MinusEquals, "-=" },
            { VMExpressionOperator.Assign, ":=" },
            { VMExpressionOperator.MulEquals, "*=" },
            { VMExpressionOperator.DivEquals, "/=" },
            { VMExpressionOperator.IsFlagSet, "Has Flag:" },
            { VMExpressionOperator.SetFlag, "Set Flag:" },
            { VMExpressionOperator.ClearFlag, "Clear Flag:" },
            { VMExpressionOperator.IncAndLessThan, "++ and <" },
            { VMExpressionOperator.ModEquals, "%=" },
            { VMExpressionOperator.AndEquals, "&=" },
            { VMExpressionOperator.GreaterThanOrEqualTo, ">=" },
            { VMExpressionOperator.LessThanOrEqualTo, "<=" },
            { VMExpressionOperator.NotEqualTo, "!=" },
            { VMExpressionOperator.DecAndGreaterThan, "-- and >" },
            { VMExpressionOperator.Push, "Push Into" },
            { VMExpressionOperator.Pop, "Pop From" },
        };

        public static Dictionary<VMExpressionOperator, PrimitiveReturnTypes> OpReturn = new Dictionary<VMExpressionOperator, PrimitiveReturnTypes>()
        {
            { VMExpressionOperator.LessThan, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.GreaterThan, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.Equals, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.PlusEquals, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.MinusEquals, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.Assign, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.MulEquals, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.DivEquals, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.IsFlagSet, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.SetFlag, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.ClearFlag, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.IncAndLessThan, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.ModEquals, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.AndEquals, PrimitiveReturnTypes.Done },
            { VMExpressionOperator.GreaterThanOrEqualTo, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.LessThanOrEqualTo, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.NotEqualTo, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.DecAndGreaterThan, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.Push, PrimitiveReturnTypes.TrueFalse },
            { VMExpressionOperator.Pop, PrimitiveReturnTypes.TrueFalse },
        };

        public override string GetBody(EditorScope scope)
        {
            var op = (VMExpressionOperand)Operand;
            var result = new StringBuilder();

            result.Append(scope.GetVarScopeName(op.LhsOwner));
            result.Append(" ");
            result.Append(op.LhsData);
            result.Append(" ");
            result.Append(OperatorStr[op.Operator]);
            result.Append(" ");
            result.Append(scope.GetVarScopeName(op.RhsOwner));
            result.Append(" ");
            result.Append(op.RhsData);
            result.Append(" ");

            return result.ToString();
        }

    }
}

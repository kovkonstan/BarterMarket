using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarterMarket.Logic.Enums
{
	public enum OperationTypeEnum
	{
		None = 0,
		IncrementCash = 1,
        BuyoutCertificates = 2,
		GettingCertificates = 3,
        DecrementCash = 4,
        Other
	}
}

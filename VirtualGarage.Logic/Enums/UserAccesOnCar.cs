﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Logic.Enums
{
    public enum UserAccesOnCar 
	{ 
		None = 0,
		Own, 
		Manage,
 		Transmitted,
		Read, 
		Close 
	}
}
﻿using System;

namespace TPW.Logic;

public class BallsLogicException : Exception
{ }

public class PositionIsOutOfBoardException : BallsLogicException
{ }
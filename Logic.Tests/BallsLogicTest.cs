﻿using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;

namespace TPW.Logic.Tests;

public class BallsLogicTest
{
	private BallsLogicLayerAbstractApi ballsLogic;
	private readonly Vector2 boardSize = new Vector2(150, 50);
	
	[SetUp]
	public void SetUp()
	{
		ballsLogic = BallsLogicLayerAbstractApi.CreateBallsLogic(boardSize);
	}

	[Test]
	public void AddBallTest()
	{
		ballsLogic.AddBall(boardSize/2);
		Assert.AreEqual(1, ballsLogic.GetBallsCount());
		Assert.AreEqual(boardSize/2, ballsLogic.GetBalls()[0].Position);
	}
	
	[Test]
	public void AddBallOutOfBoardTest()
	{
		Assert.Throws<PositionIsOutOfBoardException>((() => ballsLogic.AddBall(boardSize + Vector2.One * 20)));
		Assert.Throws<PositionIsOutOfBoardException>((() => ballsLogic.AddBall(Vector2.One * - 20)));
		Assert.AreEqual(0, ballsLogic.GetBallsCount());
	}


	[Test]
	public void AddBallsTest()
	{
		ballsLogic.AddBalls(15);
		Assert.AreEqual(15, ballsLogic.GetBallsCount());
	}

	[Test]
	public void LogicBallTest()
	{
		var logicBall = BallsLogicLayerAbstractApi.CreateLogicBall(Vector2.One);
		Assert.AreEqual(Vector2.One, logicBall.Position);
	}

	[Test]
	public void SimulationTest()
	{
		var interactionCount = 0;
		ballsLogic.AddBalls(10);
		Assert.AreEqual(10, ballsLogic.GetBallsCount());

		var startPositionList = new List<Vector2>();
		for (int i = 0; i < ballsLogic.GetBallsCount(); i++)
		{
			startPositionList.Add(ballsLogic.GetBalls()[i].Position);
		}	
		
		ballsLogic.PositionChange += (sender, args) =>
		{
			interactionCount++;
			if (interactionCount > 50)
			{
				ballsLogic.StopSimulation();
			}
		};
		ballsLogic.StartSimulation();
		while (interactionCount < 50);
		Assert.AreEqual(50, interactionCount);
		for (int i = 0; i < ballsLogic.GetBallsCount(); i++)
		{
			//TODO: This test can fail
			Assert.AreNotEqual(startPositionList[i], ballsLogic.GetBalls()[i].Position);
		}
	}
	
}
// Copyright 1998-2018 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.Collections.Generic;

public class GAME2020_M2_RSN_BTTarget : TargetRules
{
	public GAME2020_M2_RSN_BTTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Game;
		ExtraModuleNames.Add("GAME2020_M2_RSN_BT");
	}
}

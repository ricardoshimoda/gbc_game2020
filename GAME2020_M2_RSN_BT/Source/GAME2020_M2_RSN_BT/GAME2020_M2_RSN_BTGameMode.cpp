// Copyright 1998-2018 Epic Games, Inc. All Rights Reserved.

#include "GAME2020_M2_RSN_BTGameMode.h"
#include "GAME2020_M2_RSN_BTCharacter.h"
#include "UObject/ConstructorHelpers.h"

AGAME2020_M2_RSN_BTGameMode::AGAME2020_M2_RSN_BTGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/SideScrollerCPP/Blueprints/SideScrollerCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		//DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}

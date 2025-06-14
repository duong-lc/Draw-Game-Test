using System;

namespace Core.Events
{
	public enum EventType
	{
		TestEvent = 0,
		LogEvent,
		RequestMainCameraChangeEvent,
		NotifyMainCameraChangeEvent,
		NotifyOutOfManaEvent,
		NotifyPreWin_KillCam_BEGIN_Event, //gets called first
		NotifyPreWin_KillCam_EXPLODE_Event,   //gets called second to wait for killcam
		NotifyPreWin_KillCam_TIMESLOW_Event,
		NotifyWinGameEvent,
		NotifyBulletMissTargetEvent,
		NotifyPauseGameEvent,
		NotifyNewCollisionWarning,
		NotifyKillCollisionWarning,
		NotifyEmergencyObstacleWarning,
		NotifyStopEmergencyObstacleWarning,
		NotifyCollectedStar,

		// OnPlayerDeathFilter,
		OnPlayerDeath,
		SnapScopeToTargetEvent,
		WarnNotNearTargetEvent,
		NotifyGameUnPauseEvent,
		BulletEnterVolumeEvent,
		Button_EnableSetting,
		Button_DisableSetting,
		BulletTimeManipulationLetGo,
		BulletTImeManipulationStarts,
		BulletTimeManipulationUpdates,
		VignetteFadeInEvent,
		VignetteFadeOutEvent,
		FadeInIndicator,
		AddCurrencyEvent,
		AddCurrencyEvent1,
		//Buttons
		Button_RestoreManaEvent,
		OnManaRefilledEvent,
		Button_RestartLevelEvent, //param = int

		//1 = watch ad and doesn't subtract energy;
		//-1 = restart without ad, subtract 1 energy
		Button_ReturnToMenuEvent,
		Button_ContinueEvent,
		Button_PreFireFromScopeEvent,
		Button_FireFromScopeEvent,
		Button_PostFireFromScopeEvent_Cinematic,

		Link_UpdateRangeFinderCallback_Event,
		Link_ToggleGlowCallback_Event,
		Link_FireOrSnapButtonCallback_Event,
		Link_UpdateTiltSliderValueCallback_Event,

		ShopTabClicked,
		ShopClosed,

		StaminaChanged,
		MainMenu_EnableMissionSelectionScreenEvent,
		AddressableLoadedInGameplayScene,

		InjectPowerUpEvent,

		//Advertisement reward events
		OnAdRestartRewardEvent,
		ToggleTargetIndicator,
        OnBattleUIFirstShown,
        OnTogglePlayerInteractability, //true = resume functionality affected by Tut cover, false = pause
        
        OnBoosterUIStateChanged,
        
        OnAdWatchedSuccess,
        
        NotifyPreWinGameEvent,
        RunPreview,
        EndPreview,
        PreviewLerpComplete,
        
        NotifyLevelStart_Analytics,
        NotifyCurrencyEarned_Analytics,
        NotifyCurrencySpent_Analytics,
        
		NotifyRewardAds_TryShow_Analytics,
        NotifyRewardAds_Load_Analytics,
        NotifyRewardAds_Offer_Analytics,
		NotifyRewardAds_FailLoad_Analytics,
        NotifyRewardAds_FailDisplay_Analytics,
        NotifyRewardAds_Show_Analytics,
        NotifyRewardAds_Click_Analytics,
        NotifyRewardAds_Complete_Analytics,
		NotifyRewardAds_RevenuePaid_Analytics,
		NotifyRewardAds_TryShowWhenNotReady_Analytics,
        NotifyRewardAds_DuplicatedAdShowRequest_Analytics,
        NotifyRewardAds_AdShowDelayed_Analytics,
        NotifyRewardAds_Rejected_Analytics,
		NotifyRewardAds_ShowAdsImmediately_Analytics,
		NotifyRewardAds_NoInternetToShowAds_Analytics,
		NotifyRewardAds_UnknownReasonWhenShowingAds_Analytics,
		NotifyRewardAds_ShowAdsAfterWaiting_Analytics,
		NotifyRewardAds_CannotShowAdsAfterWaiting_Analytics,
		NotifyRewardAds_CannotShowAdsDueToTimeout_Analytics,
        
        NotifyInterAds_TryShow_Analytics,
        NotifyInterAds_Load_Analytics,
        NotifyInterAds_Offer_Analytics,
        NotifyInterAds_FailLoad_Analytics,
        NotifyInterAds_FailDisplay_Analytics,
        NotifyInterAds_Show_Analytics,
        NotifyInterAds_Click_Analytics,
        NotifyInterAds_Complete_Analytics,
		NotifyInterAds_RevenuePaid_Analytics,
		NotifyInterAds_TryShowWhenNotReady_Analytics,
        NotifyInterAds_DuplicatedAdShowRequest_Analytics,
        NotifyInterAds_AdShowDelayed_Analytics,
        NotifyInterAds_Rejected_Analytics,
        NotifyInterAds_ShowAdsImmediately_Analytics,
        NotifyInterAds_NoInternetToShowAds_Analytics,
        NotifyInterAds_UnknownReasonWhenShowingAds_Analytics,
        NotifyInterAds_ShowAdsAfterWaiting_Analytics,
        NotifyInterAds_CannotShowAdsAfterWaiting_Analytics,
        NotifyInterAds_CannotShowAdsDueToTimeout_Analytics,
        
		NotifyIAP_Spent_Analytics,
        NotifyIAP_SendHistory_Analytics,
        NotifyHighestUnlockedMission_Analytics,
        NotifyConsumeStamina_Fail_Analytics
	}
}
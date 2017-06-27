#pragma once

#import <CoreMedia/CMTime.h>

@class AVPlayer;


@interface CustomVideoPlayerView : UIView {}
@property(nonatomic, retain) AVPlayer* player;
@end

@protocol CustomVideoPlayerDelegate<NSObject>
- (void)onPlayerReady;
- (void)onPlayerDidFinishPlayingVideo;
@end

@interface CustomVideoPlayer : NSObject
{
    id<CustomVideoPlayerDelegate> delegate;
}
@property (nonatomic, retain) id delegate;

+ (BOOL)CanPlayToTexture:(NSURL*)url;

- (BOOL)loadVideo:(NSURL*)url;
- (BOOL)readyToPlay;
- (void)unloadPlayer;

- (BOOL)playToView:(CustomVideoPlayerView*)view;
- (BOOL)playToTexture;
- (BOOL)isPlaying;

- (intptr_t)curFrameTexture;

- (void)pause;
- (void)resume;

- (void)rewind;
- (void)seekToTimestamp:(CMTime)time;
- (void)seekTo:(float)timeSeconds;

- (BOOL)setAudioVolume:(float)volume;

- (CMTime)duration;
- (float)durationSeconds;
- (float)curTimeSeconds;
- (CGSize)videoSize;
- (void)setTextureID:(intptr_t)id;
@end

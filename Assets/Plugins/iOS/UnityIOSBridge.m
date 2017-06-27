#import "UnityIOSBridge.h"
void messageFromUnity(char *message)
{
NSString *messageFromUnity = [NSString stringWithUTF8String:message];
NSLog(@"%@",messageFromUnity);
}
@implementation UnityIOSBridge
@end
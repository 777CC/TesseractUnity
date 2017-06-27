//import the basics.
#import "Foundation/Foundation.h"

//start interface.
@interface MyNativePlugin : NSObject  //extend from basic object.

									  //define variables.
	@property (retain, nonatomic) NSString* dummyString;

//define methods.
-(id)init;
-(void)doMyNativePluginStuff: (NSString*)passed_name;

//end interface.
@end*/
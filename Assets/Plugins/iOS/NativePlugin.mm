//import the header file.
#import "MyNativePlugin.h"

//start implementation.
@implementation MyNativePlugin

//synthesize variables.
@synthesize dummyString;

//define methods.
-(id) init {
   return self;
}

-(void) doMyNativePluginStuff: (NSString*) passed_name {

  //Do something with the passed name.
  //Here we just log the passed_name
  NSLog( @"%@", passed_name );

}

//end implementation.
@end
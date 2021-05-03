# ForcedInitialization
A c# hack to manually force static constructors to fire.

# Usage
Target classes by having them inherit from IForceInitialize. Run ForceInitializer.Initialize() to trigger the initialization

# Ok but why tho?
I had a project with some singletons that were entirely event driven. The lack of outside references meant that the classes were never getting initialized, so they could never subscribe to any events. This was a universal solution to create a hook into these classes without needing to explicitly reference them.

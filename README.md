# Super-Mario-2
This is my own rendition of Super Mario 2. No, I'm not trying to sell it, just test my skills and recreate it, as I learn more. I'm not even planning on releasing it. It's just here.

I've always wanted to recreate Super Mario 2. It's my favorite. As I keep learning more and more about Unity and c#, I want to keep adding and making changes here on GitHub.

So far, I have :
- The music for the first level.
- an animated tuft of grass.
- A palm Tree (to test jumping height).
- The ground of the first level.
- Animated Mario for walking, running, and jumping.

Some Issues, I have working with:
---------------------------------
- Jumping. I have the closest formula worked yet. You can see it in the Player.cs script. I have a character controller attached to Mario (for more control). I have him doing a normal jump when you only press the space bar, and I have him jumping higher, when you hold the spacebar down past a certain point. It's not perfect, though. Sometimes, he does a half jump, and sometimes, he barely lifts off the ground. Gotta get jumping down pat.

- Animation. Man, animation is a many splendored thing, but the only way I know now to make it happen, is to create a trigger or a bool in the animation builder, so the animation is "triggered" when you, say, press Spacebar to jump. You press it, Mario goes up, but the animation is a tad late, and even later to quit when he lands. The same when he finishes his walking/running. 

using UnityEngine.UI;

namespace JacobHomanics.Core.SuperchargedVector2.UI
{
    public class Vector2Text : Vector2TextBase
    {
        public Text text;

        public void Update()
        {
            var theString = text.text;
            SetText(ref theString, displayType, format);
            text.text = theString;
        }
    }
}
using UnityEngine;

namespace Assets.Scripts
{
    public class CartItemScript : MonoBehaviour
    {


        public void Start()
        {
                
        }

        public void OnMouseUp()
        {
            var item = transform.GetComponentInParent<CartScript>().TakeItem(this);

            LocalGameService.Get.Player.DropItem(item);
        }

    }
}
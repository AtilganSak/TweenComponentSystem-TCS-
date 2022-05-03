using DG.Tweening;
using UnityEngine;

namespace TweenComponentSystem
{
    public class DORotate : DOBase
    {
        public Vector3 startValue;
        public Vector3 endValue = Vector3.one;

        public RotateMode rotateMode;

        public bool local;

        public override void DO()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                if (!local)
                    tween = c_Transform.DORotate(endValue, doDuration, rotateMode).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());
                else
                    tween = c_Transform.DOLocalRotate(endValue, doDuration, rotateMode).SetDelay(doDelay).SetEase(doEase).OnComplete(() => doComplete.Invoke());

                base.DO();
            }
            else
            {
                if (!local)
                    transform.rotation = Quaternion.Euler(endValue);
                else
                    transform.localRotation = Quaternion.Euler(endValue);
            }
        }
        public override void DORevert()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                if (!local)
                    tween = c_Transform.DORotate(startValue, doDuration, rotateMode).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());
                else
                    tween = c_Transform.DOLocalRotate(startValue, doDuration, rotateMode).SetDelay(revertDelay).SetEase(twoEase ? revertEase : doEase).OnComplete(() => doRevertComplete.Invoke());

                base.DORevert();
            }
            else
            {
                if (!local)
                    transform.rotation = Quaternion.Euler(startValue);
                else
                    transform.localRotation = Quaternion.Euler(startValue);
            }
        }
        public override void ResetDO()
        {
            transform.DOKill(true);
            if (!local)
                transform.rotation = Quaternion.Euler(startValue);
            else
                transform.localRotation = Quaternion.Euler(startValue);
        }
        public override void DOLoop()
        {
            if (Application.isPlaying)
            {
                if (!allowTwin)
                {
                    if (DOTween.IsTweening(c_Transform))
                        c_Transform.DOKill(true);
                }
                if (!local)
                    tween = c_Transform.DORotate(endValue, doDuration, rotateMode).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
                else
                    tween = c_Transform.DOLocalRotate(endValue, doDuration, rotateMode).SetDelay(doDelay).SetEase(doEase).SetLoops(-1, loopType);
            }
            else
            {
                if (!local)
                    transform.rotation = Quaternion.Euler(endValue);
                else
                    transform.localRotation = Quaternion.Euler(endValue);
            }
        }
        public override void Kill()
        {
            if (DOTween.IsTweening(transform))
                transform.DOKill();
        }
    }
}

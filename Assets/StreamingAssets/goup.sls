#!r6rs
(library (goup)
  (export spawn-block
          vec3)
  (import (rnrs)
          (ironscheme clr)
          (ironscheme conversions))
  (clr-using UnityEngine)
  (clr-using SchemeHelpers)

  (define (vec3 x y z)
    (clr-new Vector3
             (->single x)
             (->single y)
             (->single z)))

  (define (spawn-block x y z scale-x scale-y scale-z)
    (clr-static-call SchemeHelpers CreateGoup
                     (vec3 x y z)
                     (vec3 scale-x scale-y scale-z)))

  ;; Wrapping c# is sooo much easier...
  ;; (define (spawn-block x y z scale-x scale-y scale-z)
  ;;   (let* ((block
  ;;           (clr-static-call PhotonNetwork Instantiate
  ;;                            "TestGoup"
  ;;                            (vec3 x y z)
  ;;                            (clr-static-prop-get Quaternion identity)
  ;;                            0))
  ;;          (pv (clr-call GameObject GetComponent block "PhotonView"))
  ;;          (scale (vec3 scale-x scale-y scale-z))
  ;;          (mass (* (clr-prop-get Vector3 magnitude scale) 2)))
  ;;     (clr-call PhotonView RPC (clr-cast PhotonView pv)
  ;;               "Setup"
  ;;               (clr-static-field-get PhotonTargets AllBuffered)
  ;;               scale
  ;;               mass)))
)

import "https://cdn.interactjs.io/v1.9.20/auto-start/index.js";
import "https://cdn.interactjs.io/v1.9.20/actions/drag/index.js";
import "https://cdn.interactjs.io/v1.9.20/actions/resize/index.js";
import "https://cdn.interactjs.io/v1.9.20/modifiers/index.js";
import "https://cdn.interactjs.io/v1.9.20/dev-tools/index.js";
import interact from "https://cdn.interactjs.io/v1.9.20/interactjs/index.js";

interact(".draggable").draggable({
  inertia: true,
  startAxis: 'y',
  lock: 'y',
  modifiers: [
    interact.modifiers.restrictRect({
      restriction: "parent",
      endOnly: true,
    }),
  ],
  onmove: dragMoveListener,
});

interact(".drop-zone").dropzone({
  accept: ".draggable",
  overlap: 0.5,
  ondrop: function (event) {
    const targetId = event.relatedTarget.dataset.cardid;
    const srcId = event.target.dataset.cardid;
    const boardId = event.target.dataset.boardid;
    const cardGroupId = event.target.dataset.cardGroupId;

    requestToSwap(srcId, targetId, boardId, cardGroupId);
  },
});

function dragMoveListener(event) {
  var target = event.target;
  // keep the dragged position in the data-x/data-y attributes
  var x = (parseFloat(target.getAttribute("data-x")) || 0) + event.dx;
  var y = (parseFloat(target.getAttribute("data-y")) || 0) + event.dy;

  // translate the element
  target.style.transform = "translate(" + x + "px, " + y + "px)";

  // update the posiion attributes
  target.setAttribute("data-x", x);
  target.setAttribute("data-y", y);
}

async function requestToSwap(srcid, targetid, boardId, cardGroupid) {
  let res = await fetch(`/Board/${boardId}/CardGroup/${cardGroupid}/SwapCards/?first=${srcid}&second=${targetid}`, {
    method: "POST"
  });

  if (res.redirected) {
      window.location.href = res.url;
  }
}

import "https://cdn.jsdelivr.net/npm/sortablejs@latest/Sortable.min.js";

console.log(Sortable);

let lists = document.querySelectorAll(".list-group");

console.log(lists);

lists.forEach((list) => {
  let sortable = new Sortable(list, {
    group: "shared",
    animation: 100,
    onAdd: function (e) {
      const { boardid, cardGroupId, cardid } = e.item.dataset;
      const targetCardGroupId = e.to.dataset.cardGroupId;
      const targetCardIds = Array.from(e.to.children).map(
        (c) => c.dataset.cardid
      );
      console.log(targetCardIds);
      transferCard(
        boardid,
        cardGroupId,
        cardid,
        targetCardGroupId,
        targetCardIds
      );
    },
    onEnd: function (e) {
      if (e.from === e.to) {
        const { boardid, cardGroupId } = e.from.children[0].dataset;
        const cardIds = Array.from(e.from.children).map(
          (c) => c.dataset.cardid
        );
        repositionCards(boardid, cardGroupId, cardIds);
      } else {
        console.log("not same");
      }
    },
  });
});

async function requestToSwap(srcid, targetid, boardId, cardGroupid) {
  let res = await fetch(
    `/Board/${boardId}/CardGroup/${cardGroupid}/SwapCards/?first=${srcid}&second=${targetid}`,
    {
      method: "POST",
    }
  );
}

async function repositionCards(boardId, cardGroupid, cardIds) {
  let res = await fetch(
    `/Board/${boardId}/CardGroup/${cardGroupid}/RepositionCards/?cardIds=${cardIds.join(
      ","
    )}`,
    {
      method: "POST",
    }
  );

  console.log(await res.json());
  window.location.href = window.location.href;
}

async function transferCard(
  boardId,
  cardGroupid,
  cardId,
  targetCardGroupId,
  targetCardIds
) {
  let res = await fetch(
    `/Board/${boardId}/CardGroup/${cardGroupid}/TransferCard/?cardId=${cardId}&targetCardGroupId=${targetCardGroupId}`,
    {
      method: "POST",
    }
  );
  let resData = await res.json();
  if (resData.success) {
    await repositionCards(boardId, targetCardGroupId, targetCardIds);
    return;
  }

  window.location.href = window.location.href;

}

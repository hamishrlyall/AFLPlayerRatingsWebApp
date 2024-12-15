import React, { useEffect, useState } from "react";
import PlayerItem from "./player-item";
import ReactPaginate from "react-paginate";

const PlayerList = () => {
    const[players, setPlayers] = useState(null);
    const[playerCount, setPlayerCount] = useState(0);
    const[page, setPage] = useState(0);

    useEffect(() => {
        // get all players
        getPlayers()
    }, [page]);

    const getPlayers = () => {
        fetch(process.env.REACT_APP_API_URL  +
             "/Player?_PageIndex=" + page + "&_PageSize=" + process.env.REACT_APP_PAGING_SIZE )
        .then( res => res.json() )
        .then( res => {
            if( res.status === true && res.data.count > 0 ){
                setPlayers( res.data.players );
                setPlayerCount(
                    Math.ceil(res.data.count / process.env.REACT_APP_PAGING_SIZE)
                );
            }

            if( res.data.count === 0 ){
                alert("There is no player data in system");
            }
        }).catch( err => alert("Error getting data"));
    }

    const handlePageClick = (pageIndex) => {
        setPage(pageIndex.selected);
    }

    return (
        <>
            {players && players.Count !== 0 ? 
            players.map(( p, i) => <PlayerItem key={i} data={p}/>)
            : ""}

            <div className="d-flex justify-content-center"> 
                <ReactPaginate
                    previousLabel={'previous'}
                    nextLabel={'next'}
                    breakLabel={'...'}
                    breakClassName={'page-link'}
                    pageCount={playerCount}
                    marginPagesDisplayed={2}
                    pageRangeDisplayed={5}
                    onPageChange={handlePageClick}
                    containerClassName={'pagination'}
                    pageLinkClassName={'page-link'}
                    previousClassName={'page-link'}
                    nextClassName={'page-link'}
                    activeClassName={'active'}
                /> 
            </div>
        </>
    )
}

export default PlayerList;
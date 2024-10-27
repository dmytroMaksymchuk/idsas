import {useEffect, useState} from 'react';
import './App.css';
import Header from "./components/Header.tsx";
import UploadFilePopUp from "./components/UploadFilePopUp.tsx";
import MyDocumentsList, {ShareState, MyDocument} from "./components/MyDocumentList.tsx";
import SharedDocumentList, {SharedDocument} from "./components/SharedDocumentList.tsx";

function App() {
    const [selectedFile, setSelectedFile] = useState(null);
    const [uploadFileActive, setUploadFileActive] = useState(false);
    const [userIndex, setUserIndex] = useState(0);

    //const usersTokens = [
    //    '0x1234567890',
    //    '0x0987654321',
    //    '0xabcdef1234',
    //]

    const [myDocuments, setMyDocuments] = useState<MyDocument[]>([
        { name: 'Document1.pdf', shareState: ShareState.shared },
        { name: 'Document2.docx', shareState: ShareState.private },
        { name: 'Presentation.pptx', shareState: ShareState.waitingConfirmation },
    ]);
    
    const [sharedDocuments, setSharedDocuments] = useState<SharedDocument[]>([
        { name: 'Document3.pdf', available: true },
        {
            name: 'Document4.docx',
            available: false
        },
        {
            name: 'Presentation2.pptx',
            available: false
        },
    ]);

    const handleSignDocument = () => {
        console.log('Signing document');
        setUploadFileActive(false);
        handleFileUpload();
    }
    const handleFileChange = (event: any) => {
        setSelectedFile(event.target.files[0]);
    };
    const onDownloadFile = (index: number) => {
        console.log('Downloading file:', index);
    };
    const fetchDocuments = async () => {
        // Fetch the documents from the server
        fetch('api/document/document', {
            method: 'GET',
        })
        .then((response) => response.json())
        .then((data) => {
            console.log('Documents fetched successfully:', data);
            setMyDocuments(data.myDocuments);
            setSharedDocuments(data.sharedDocuments);
        })
        .catch((error) => {
            console.error('Error fetching documents:', error);
        });
        console.log('Fetching documents...');
    }


    // Handle file upload (this can be customized to your API or upload logic)
    const handleFileUpload = () => {
        if (selectedFile) {
            // Create a FormData object to send the file to the server
            const formData = new FormData();
            formData.append('file', selectedFile);

            // Send the file to the server
            fetch('http://localhost:32768/api/document/upload', {
                method: 'POST',
                body: formData,
            })
            .then((response) => response.json())
            .then((data) => {
                console.log('File uploaded successfully:', data);
            })
            .catch((error) => {
                console.error('Error uploading file:', error);
            });
        } else {
            console.log('No file selected');
        }
    };

    // useEffect(() => {
    //     const testAPI = async () => {
    //         try {
    //             // Construct the URL with query parameters
    //             const response = await fetch(`http://localhost:32768/api/document/document?documentId=${encodeURIComponent("24324")}&userToken=${encodeURIComponent("234234")}`, {
    //                 method: 'GET',
    //             });
    //             console.log('Response:', response);
    //
    //             // Check if the response is OK (status code 200-299)
    //             if (!response.ok) {
    //                 throw new Error(`HTTP error! Status: ${response.status}`);
    //             }
    //
    //             const data = await response.json();
    //             console.log('Test API response:', data);
    //         } catch (error) {
    //             console.error('Error fetching document:', error);
    //         }
    //     };
    //
    //     testAPI();
    // }, []);

    const onAccessSharedFile = (token: string) => {
        console.log('Accessing shared file with token:', token);
    };

    useEffect(() => {
        // Fetch the documents when the app loads
        fetchDocuments();
    }, []);

    useEffect(() => {
        // Refresh the documents when the user index changes
        // Set all flags to false
    }, [userIndex]);

    return (
        <div className="App">
            <Header uploadFileActive={uploadFileActive} setUploadFileActive={setUploadFileActive} userIndex={userIndex} setUserIndex={setUserIndex}/>
            {uploadFileActive &&
                <UploadFilePopUp handleFileChange={handleFileChange} onSign={handleSignDocument} setUploadFileActive={setUploadFileActive}/>
            }
            {!uploadFileActive &&
                <>
                <img className={"refresh-button"} src={"./refresh.svg"} alt={"refresh"} onClick={fetchDocuments} />
                <MyDocumentsList title={"My Documents"} documents={myDocuments} onDownloadFile={onDownloadFile} confirmDocument={() => { }} rejectDocument={() => { }} />
                    <SharedDocumentList title={"Documents Shared With Me"} documents={sharedDocuments}
                                        onDownloadFile={() => console.log("Download")} onFileAccess={onAccessSharedFile}/>
                </>
            }
        </div>
    );
}

export default App;
